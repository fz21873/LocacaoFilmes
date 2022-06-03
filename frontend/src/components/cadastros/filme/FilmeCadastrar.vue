<template>
  <v-container>
    <painelCadastro :titulo="titulo" :erros-servidor="errosServidor" :carregando-valores="carregandoValores" :alerta.sync="alerta" :alerta-cor="alertaCor" :alerta-cor-texto="alertaCorTexto" :alerta-icone="alertaIcone" :alerta-texto="alertaTexto" :enviando-dados="enviandoDados" @botaoSalvarClicado="salvarDiretor()" @novaAbaVoltarClicado='voltar(true)' @botaoVoltarClicado="voltar()" @teclaPressionada="tratarTeclado($event)" :mostrar-botao-detalhes="botaoAlteracoes" :criado-em="criadoEm" :criado-por="criadoPor" :alterado-em="alteradoEm" :alterado-por="alteradoPor">

        <v-form v-model="formularioValido" ref="form">
            <v-container>
               <v-row  xs8 row  wrap>
                   <v-col  xs8 sm6 md2>

                      <v-text-field ref="titulo" id="titulo" v-model="filme.titulo" label="Titulo" :rules="regrasTitulo" :autofocus="true" autocomplete="off" ></v-text-field>

                   </v-col>

                    <v-col  xs8 sm6 md2>

                      <v-text-field ref="descricao" id="descricao" v-model="filme.descricao" label="Descrição" :rules="regrasDescricao" :autofocus="true" autocomplete="off" ></v-text-field>

                   </v-col>

                   <v-col  xs12 sm6 md2>

                      <v-text-field id="dataLancamento" ref="dataLancamentoText" label="Data de Lancamento" disable="false" :rules="regrasDataLancamento" v-model="auxDataLancamento" @keydown.stop.prevent.32="modalDataLancamento = true" clearable @click:clear="limparDataLancamento" hint="(Barra de espaço para abrir)" append-icon="calendar_today" @click:append="modalDataLancamento = true" v-mask="'##/##/####'" ></v-text-field>
                       <v-dialog ref="modalDataLancamento" :close-on-content-click="false" v-model="modalDataLancamento" :return-value.sync="dataLancamento" transition="scale-transition" omit width="290px">
                      <v-date-picker min="1900" max="2100" v-model="dataLancamento" @input="selecionarDataLancamento($event)" locale="pt-br" color="primary" ref="picker"></v-date-picker>
                      </v-dialog>
                   </v-col>

               </v-row>


            </v-container>
        </v-form>

    </painelCadastro>
      </v-container>
</template>
<script>
    import CadastroMixin from '../../../mixins/CadastroMixin.js';
    import Filme from '../../../domain/filme/Filme';
    import FilmeService from '../../../domain/filme/FilmeService';
    import PainelCadastro from '../../shared/paineis/PainelCadastro.vue';
    import moment from 'moment';
    export default {
         mixins: [CadastroMixin],
        components: {
            'painelCadastro': PainelCadastro,

        },
        data() {
            return {
                indexModeloEdicao: -1,


                filme: new Filme(),
                titulo: 'Cadastrar filme', // Título da página.
                formularioValido: false,
                diretorServico: null,

                regrasTitulo: [
                    v => !!v || 'O titulo é obrigatória',
                ],

                 regrasDescricao: [
                    v => !!v || 'A descricao é obrigatória',
                ],

                  regrasDataLancamento: [v => {
                        if (v) {
                            if (Util.validaData(v, false) == false) {

                                 return 'Informe uma data válida';
                            }
                            return true;
                        } else {
                            return 'Data inicial é obrigatória';
                        }



                }],

            };
        },

    methods: {

            selecionarDataLancamento(e) {

                 this.modalDataLancamento = false;
                 this.$refs.modalDataLancamento.save(e);
                 this.filme.dataLancamento = moment(e).format('YYYY-MM-DD');
                 this.auxDataLancamento = moment(e).format('DD/MM/YYYY');

            },

            tratarTeclado(e) {
                if (!this.modalConfirmacao) {
                    //ENTER
                    if (e.keyCode == '13' || e.which == '13') {
                        this.salvarMarca();
                    }
                    //ESC
                    if (e.keyCode == '27' || e.which == '27') {
                        this.voltar();
                    }
                }
            },
            voltar(novaAba = false) {
                //se foi modificado mostra mensagem de confirmacao, se nao, volta sem confirmar
                if (this.modificado ) {
                    this.modalConfirmacao = true;
                   Util.exibirConfimacao('As alterações serão perdidas. Realmente deseja voltar?', {
                        buttonTrueText: 'Sim',
                        buttonFalseText: 'Não',
                        color: 'warning',
                        icon: 'warning',
                        title: 'Atenção!'
                    }).then(resposta => {
                        if (resposta) {
                            if (novaAba) {
                                window.open('/filme/listar', '_blank');
                            } else {
                                this.$router.push('/filme/listar');
                            }
                        }
                        setTimeout(() => {
                            this.modalConfirmacao = false;
                        }, 500);
                    });
                } else {
                    if (novaAba) {
                        window.open('/filme/listar', '_blank');
                    } else {
                        this.$router.push('/filme/listar');
                    }
                }
            },
            salvarFilme() {

               if (!this.enviandoDados) {
                    if (!this.$refs.form.validate()) {
                        this.alertaCor = 'error';
                        this.alertaIcone = 'warning';
                        this.alertaCorTexto = 'white';
                        this.alertaTexto = 'Por favor, corrija os erros no formulário.';
                        this.alerta = true;
                    } else {

                        this.enviandoDados = true;

                        //se estiver cadastrando

                        if (!this.paramId) {
                            this.filmeServico.create(this.filme)
                                .then(resposta => {
                                    // Mostra uma mensagem de sucesso.
                                    this.alertaCor = 'success';
                                    this.alertaIcone = 'done';
                                    this.alertaCorTexto = 'white';
                                    this.alertaTexto = 'O filme ' + this.filme.Titulo+ ' foi cadastrada com sucesso!';
                                    this.alerta = true;

                                    // Reseta todos os valores do formulário.
                                    this.$refs.form.resetValidation();

                                    this.filme = new Filme();
                                    this.$refs.nome.focus();
                                    this.enviandoDados = false;
                                    this.errosServidor = null;
                                    this.acabouDeSalvar = true;
                                }, err => {
                                    if (err.status == this.$codigoErroValidacao) {
                                        this.errosServidor = err.body;

                                    }
                                    console.error(err);
                                    this.alertaCor = 'error';
                                    this.alertaCorTexto = 'white';
                                    this.alertaIcone = 'warning';
                                    this.alertaTexto =
                                        'Desculpe-nos, mas não foi possível cadastrar o filme.';
                                    this.alerta = true;

                                    this.enviandoDados = false;
                                });
                        } else {
                            //edicao

                            this.filmeServico.update(this.filme)
                                .then(resposta => {
                                        this.$router.push({
                                            path: '/filme/listar',
                                            params: {
                                                mensagem: 'O filme ' +
                                                    this.diretor.filme +
                                                    ' foi editada com sucesso!'
                                            }
                                        });
                                        this.enviandoDados = false;
                                        this.errosServidor = null;
                                    },
                                    err => {
                                        if (err.status == this.$codigoErroValidacao) {
                                            this.errosServidor = err.body;
                                        }
                                        this.alertaCor = 'error';
                                        this.alertaCorTexto = 'white';
                                        this.alertaIcone = 'warning';
                                        this.alertaTexto =
                                            'Desculpe-nos, mas não foi possível editar o filme.';
                                        this.alerta = true;
                                        this.enviandoDados = false;
                                    }
                                );
                        }
                    }
                }
            }
        },
        created() {
            this.filmeServico= new FilmeService(this.$resource);
            this.paramId = this.$route.params.id;
            this.titulo = this.paramId ?
                'Editar filme' :
                'Cadastrar filme';

            //se for edicao
            if (this.paramId) {
                this.filmeServicoServico
                .getById(this.paramId)
                    .then(film => {
                        this.filme = film;


                        this.$watch('filme', function() {
                            this.modificado = true;
                        }, {
                            deep: true
                        });

                        this.carregandoValores = false;

                        //botao detalhes do registro
                        this.botaoAlteracoes = true;





                    }, err => {
                        this.alertaTexto = 'Não foi possível carregar o filme.';
                        this.alerta = true;
                        this.carregandoLista = false;
                    });
            } else { //cadastro
                this.$watch('filme', function() {
                    this.modificado = true;
                }, {
                    deep: true
                });
            }
        }
    };
</script>

<style>
    @media screen and (max-width: 959px) {
        .cartao-modelo-responsivo {
            margin-bottom: 3%;
        }

        .editar-modelo {
            margin-left: 0;
        }

        .excluir-modelo {
            margin-right: 0;
        }
    }

    @media screen and (min-width: 600px) and (max-width: 959px) {
        .cartao-modelo-responsivo {
            float: left;
            margin-left: 1%;
            margin-right: 1%;
            width: 48% !important;
        }
    }
</style>
