<template>
  <v-container>
    <painelCadastro :titulo="titulo" :erros-servidor="errosServidor" :carregando-valores="carregandoValores" :alerta.sync="alerta" :alerta-cor="alertaCor" :alerta-cor-texto="alertaCorTexto" :alerta-icone="alertaIcone" :alerta-texto="alertaTexto" :enviando-dados="enviandoDados" @botaoSalvarClicado="salvarDiretor()" @novaAbaVoltarClicado='voltar(true)' @botaoVoltarClicado="voltar()" @teclaPressionada="tratarTeclado($event)" :mostrar-botao-detalhes="botaoAlteracoes" :criado-em="criadoEm" :criado-por="criadoPor" :alterado-em="alteradoEm" :alterado-por="alteradoPor">

        <v-form v-model="formularioValido" ref="form">
            <v-container>
               <v-row  xs8 row  wrap>
                   <v-col  xs8 sm6 md2>

                      <v-text-field ref="nome" id="nome" v-model="diretor.nome" label="Nome" :rules="regrasNome" :autofocus="true" autocomplete="off" ></v-text-field>

                   </v-col>

                    <v-col  xs8 sm6 md2>

                      <v-text-field ref="sobrenome" id="sobrenome" v-model="diretor.sobreNome" label="Sobre Nome" :rules="regrasSobreNome" :autofocus="true" autocomplete="off" ></v-text-field>

                   </v-col>

               </v-row>


            </v-container>
        </v-form>

    </painelCadastro>
      </v-container>
</template>
<script>
    import CadastroMixin from '../../../mixins/CadastroMixin.js';
    import Director from '../../../domain/diretor/Diretor';
    import DiretorService from '../../../domain/diretor/DiretorService';
    import PainelCadastro from '../../shared/paineis/PainelCadastro.vue';
    export default {
         mixins: [CadastroMixin],
        components: {
            'painelCadastro': PainelCadastro,

        },
        data() {
            return {
                indexModeloEdicao: -1,


                diretor: new Director(),
                titulo: 'Cadastrar diretor', // Título da página.
                formularioValido: false,
                diretorServico: null,

                regrasNome: [
                    v => !!v || 'O nome é obrigatória',
                ],

                 regrasSobreNome: [
                    v => !!v || 'O sobre nome é obrigatória',
                ],

            };
        },

    methods: {


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
                                window.open('/diretor/listar', '_blank');
                            } else {
                                this.$router.push('/diretor/listar');
                            }
                        }
                        setTimeout(() => {
                            this.modalConfirmacao = false;
                        }, 500);
                    });
                } else {
                    if (novaAba) {
                        window.open('/diretor/listar', '_blank');
                    } else {
                        this.$router.push('/diretor/listar');
                    }
                }
            },
            salvarDiretor() {

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
                            this.diretorServico.create(this.diretor)
                                .then(resposta => {
                                    // Mostra uma mensagem de sucesso.
                                    this.alertaCor = 'success';
                                    this.alertaIcone = 'done';
                                    this.alertaCorTexto = 'white';
                                    this.alertaTexto = 'O diretor ' + this.diretor.nome+ +this.diretor.SobreNome + ' foi cadastrada com sucesso!';
                                    this.alerta = true;

                                    // Reseta todos os valores do formulário.
                                    this.$refs.form.resetValidation();

                                    this.diretor = new Director();
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
                                        'Desculpe-nos, mas não foi possível cadastrar o diretor.';
                                    this.alerta = true;

                                    this.enviandoDados = false;
                                });
                        } else {
                            //edicao

                            this.diretorServico.update(this.diretor)
                                .then(resposta => {
                                        this.$router.push({
                                            path: '/diretor/listar',
                                            params: {
                                                mensagem: 'O diretor ' +
                                                    this.diretor.nome +' '+this.diretor.nome +
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
                                            'Desculpe-nos, mas não foi possível editar o diretor.';
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
            this.diretorServico= new DiretorService(this.$resource);
            this.paramId = this.$route.params.id;
            this.titulo = this.paramId ?
                'Editar diretor' :
                'Cadastrar diretor';

            //se for edicao
            if (this.paramId) {
                this.diretorServicoServico
                .getById(this.paramId)
                    .then(diretors => {
                        this.diretor = diretors;


                        this.$watch('diretor', function() {
                            this.modificado = true;
                        }, {
                            deep: true
                        });

                        this.carregandoValores = false;

                        //botao detalhes do registro
                        this.botaoAlteracoes = true;





                    }, err => {
                        this.alertaTexto = 'Não foi possível carregar o diretor.';
                        this.alerta = true;
                        this.carregandoLista = false;
                    });
            } else { //cadastro
                this.$watch('diretor', function() {
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
