<template>
    <div>
        <v-container>
            <v-container>

                <painelListagem linkVoltar="/" linkCadastrar="/filme/cadastrar" titulo="Filme">


                    <div slot="conteudo">
                        <v-container>
                            <v-layout row>
                                <listagemResponsiva :cabecalho="headers" :carregando="carregandoLista">

                                    <template slot="desktop">
                                        <v-data-table id="tabelaFilme" :headers="headers" :items="itens"
                                            :no-data-text="buscandoDadosTabela" class="elevation-1"
                                            :loading="carregandoLista">
                                            <template slot="items" slot-scope="props">
                                                <v-card-text>
                                                    <v-container>
                                                        <v-row>
                                                            <v-col cols="12" sm="6" md="4">
                                                                <v-text-field class="text-xs-left"
                                                                    v-model="props.item.id" label="ID"></v-text-field>
                                                            </v-col>

                                                            <v-col cols="12" sm="6" md="4">
                                                                <v-text-field class="text-xs-left"
                                                                    v-model="props.item.nome" label="Nome">
                                                                </v-text-field>
                                                            </v-col>

                                                            <v-col cols="12" sm="6" md="4">
                                                                <v-text-field class="text-xs-left"
                                                                    v-model="props.item.sobreNome" label="Sobre Nome">
                                                                </v-text-field>
                                                            </v-col>

                                                            <v-col cols="12" sm="6" md="4">
                                                                <v-text-field class="text-xs-left"
                                                                    v-model="props.item.categoria" label="Categoria">
                                                                </v-text-field>
                                                            </v-col>

                                                        </v-row>
                                                    </v-container>
                                                </v-card-text>

                                            </template>

                                            <template class="text-xs-center" v-slot:[`item.actions`]="{ item }">
                                                <v-tooltip top>
                                                    <template class="text-xs-center" v-slot:activator="{ on: tooltip }">
                                                        <v-btn v-on="{ tooltip }" class="editar" small icon
                                                            color="grey--text" @mousedown.middle="editar(item.id, true)"
                                                            @click="editar(item.id);">
                                                            <v-icon>edit</v-icon>
                                                        </v-btn>
                                                    </template>
                                                    <span>Editar</span>
                                                </v-tooltip>

                                                <v-tooltip top>

                                                    <template class="text-xs-center" v-slot:activator="{ on: tooltip }">

                                                        <v-btn style="margin-left:20px;" v-on="{ tooltip }"
                                                            class="excluir" small icon color="grey--text"
                                                            @click="excluir(item.id, item.nome + ' ' + item.sobreNome);">
                                                            <v-icon>delete_forever</v-icon>
                                                        </v-btn>

                                                    </template>
                                                    <span>Excluir</span>
                                                </v-tooltip>


                                            </template>




                                        </v-data-table>
                                    </template>

                                </listagemResponsiva>
                            </v-layout>

                        </v-container>
                    </div>

                </painelListagem>
            </v-container>
            <!-- Snackbar -->
            <v-snackbar v-model="alerta" :top="true" :right="true" :timeout="3000" :color="alertaCor">
                <v-icon :color="alertaTextoCor" style="margin-bottom: 1px; margin-right: 1px;">{{ alertaIcone }}</v-icon>
                {{ alertaTexto }}
                <v-btn class="margin-left-10" :color="alertaTextoCor" text @click="alerta = false;">FECHAR</v-btn>
            </v-snackbar>

            <!-- Janela modal. -->
            <modalCarregando :carregando="excluindoDados" mensagem="Excluindo diretor..." />
        </v-container>

    </div>
</template>
<script>
import CadastroMixin from '../../../mixins/CadastroMixin.js';
import Carregando from '../../shared/paineis/Carregando.vue';
import PainelListagem from '../../shared/paineis/PainelListagem.vue';
import ListagemResponsiva from '../../shared/paineis/ListagemResponsiva.vue';
import Util from '../../../domain/util/Util';
import FilmeService from '../../../domain/filme/FilmeService';

export default {
    mixins: [CadastroMixin],
    components: {

        'modalCarregando': Carregando,
        'painelListagem': PainelListagem,
        'listagemResponsiva': ListagemResponsiva,

    },
    data() {
        return {
            excluindoDados: false,
            alerta: false,
            alertaTexto: '',
            alertaIcone: '',
            alertaCor: '',
            alertaTextoCor: '',
            itens: [],

            filmeService: null,
            /// Os headers são os dados que serão exibidos
            // no cabeçalho da tabela de listagem de dados.
            headers: [{
                text: 'ID.',
                align: 'left',
                value: 'id',
                class: 'cabecalho-tabela'
            },
            {
                text: 'Titulo',
                align: 'left',
                value: 'titulo',
                class: 'cabecalho-tabela'
            },

            {
                text: 'Descrição',
                align: 'left',
                value: 'descricao',
                class: 'cabecalho-tabela'
            },

            {
                text: 'Diretor',
                align: 'left',
                value: 'nome +" "+ sobreNome',
                class: 'cabecalho-tabela'
            },

            {
                text: 'Tipo Filme',
                align: 'left',
                value: 'tipo',
                class: 'cabecalho-tabela'
            },

            {
                text: 'Data Lançamento',
                align: 'left',
                value: 'datalancamento',
                class: 'cabecalho-tabela'
            },
            {
                text: 'Ações',
                align: 'center',
                value: 'actions',
                class: 'cabecalho-tabela'
            }
            ],


        };
    },
    methods: {



        editar(id, novaAba = false) {


            if (novaAba) {

                var rota = this.$router.resolve('editar/' + id);
                window.open(rota.href);
            } else {

                this.$router.push('editar/' + id);
            }
        },
        excluir(id, filme) {
            Util.exibirConfimacao('Tem certeza que deseja excluir o filme <strong>' + filme + '</strong> permanentemente?', {
                buttonTrueText: 'Sim',
                buttonFalseText: 'Não',
                color: 'error',
                icon: 'delete_forever',
                title: 'Atenção!'
            }).then(resposta => {
                if (resposta) {
                    this.excluindoDados = true;
                    this.filmeService.delete(id)
                        .then(filmeDeletado => {

                            this.alertaTexto =
                                'filme' +
                                filmeDeletado.filme +
                                '(' +
                                filmeDeletado.id +
                                ')' +
                                ' deletada';

                            this.alertaCor = 'success';
                            this.alertaIcone = 'done';
                            this.alertaTextoCor = 'white';
                            this.alerta = true;
                            this.excluindoDados = false;
                            this.buscarDiretor();
                            if (this.itens.length == 0) {
                                this.buscandoDadosTabela = 'Nenhuma filme para exibir.';
                            }
                        }, err => {
                            this.excluindoDados = false;
                            this.alertaTexto = err.status == 502 ? 'Não foi possível deletar o filme ' + err.body + '.' : '';
                            this.alertaIcone = 'warning';
                            this.alertaCor = 'error';
                            this.alertaTextoCor = 'white';
                            this.alerta = true;
                            this.carregandoLista = false;
                        });
                }
            });
        },


        buscarFilme() {

            this.filmeService
                .getAll()
                .then(film => {

                    this.itens = film;
                    this.carregandoLista = false;

                },
                    err => {
                        console.error(err);
                        this.alertaCor = 'error';
                        this.alertaIcone = 'warning';
                        this.alertaTextoCor = 'white';
                        this.alertaTexto = 'Não foi possível carregar o filme.';
                        this.alerta = true;
                        this.carregandoLista = false;
                        this.buscandoDadosTabela = 'Não foi possível carregar o filme.';
                        if (this.itens.length === 0) {
                            this.semResultados = true;
                            this.semDados = false;
                        } else if (this.itens.length === 0) {
                            this.semResultados = false;
                            this.semDados = true;
                            this.buscandoDadosTabela = 'Nenhuma filme para exibir.';
                        }
                    }
                );
        }
    },

    created() {
        this.filmeService = new FilmeService(this.$resource);
        this.buscarFilme();



    }
};
</script>

<style>
.menu-smartphone {
    bottom: 10px;
    position: relative;
}

.situacao-smartphone {
    position: relative;
    bottom: 10px;
    right: 4px;
}
</style>

