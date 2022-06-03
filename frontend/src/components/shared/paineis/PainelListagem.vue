<template>
    <div>
        <!-- Card com título e pesquisa -->
        <v-card card--flex-toolbar>

            <v-toolbar :color="corCabecalho" text>
                <v-tooltip bottom>
                    <template v-slot:activator="{ on }">
                    <v-btn v-on="on" :class="corTextoCabecalho"   icon :to="linkVoltar">
                        <v-icon>arrow_back</v-icon>
                    </v-btn>
                    </template>
                    <span>Voltar</span>
                </v-tooltip>
                <v-toolbar-title style="margin-left: 0px !important" :class="corTextoCabecalho">{{titulo}}</v-toolbar-title>

                <v-btn  style="margin-left: 10px !important" class="botao-acao-sucesso" @mousedown.middle="cadastrarNovaAba($event)" @click="cadastrar($event)" color="success" rounded small v-if="mostrarBotaoCadastro && (linkCadastrar)">
                    <v-icon>add</v-icon>
                    {{mensagemBotaoCadastro}}
                </v-btn>


                <v-spacer></v-spacer>

                          </v-toolbar>
            <v-divider></v-divider>

            <!-- Conteudo (listagem) -->
            <slot name="conteudo"></slot>
        </v-card>

        <!-- Botão para cadastrar novo grupo. -->
        <v-tooltip left v-if="mostrarBotaoCadastro && (linkCadastrar != '' )">
             <template v-slot:activator="{ on }">
            <v-btn class="botao-acao-sucesso" v-on="on" color="success"  fixed bottom right fab ref="fab" @click="cadastrar($event)">
                <v-icon>add</v-icon>
            </v-btn>
             </template>
            <span>{{ mensagemBotaoCadastro }}</span>
        </v-tooltip>
    </div>
</template>

<script>


    export default {
        props: {
                linkVoltar: {
                type: String,
                required: true
            },
            // Rota para cadastro.
            linkCadastrar: {
                type: String,
                required: true
            },

            mensagemBotaoCadastro: {
                type: String,
                required: false,
                default: 'Novo'
            },
            //titulo do painel
            titulo: {
                type: String,
                required: true
            },
            //cor do header
            corCabecalho: {
                type: String,
                required: false,
                default: 'tecmicro'
            },
            corTextoCabecalho: {
                type: String,
                required: false,
                default: 'white--text'
            },
            //headers da tabela
            cabecalhoTabela: {
                type: Array,
                required: false
            },
            //itens da listagem
            itens: {
                required: false
            },


            desabilitaBotaoCadastrar: {
                require: true
            },
             mostrarBotaoCadastro: {
                type: Boolean,
                required: false,
                default: true
            }

        },

        methods: {

            cadastrar(e){
                this.$emit('beforeCadastrar', e);
                this.$router.push(this.linkCadastrar);
            }
        },

    };
</script>

<style>
    .cabecalhoTabela {
        font-size: 15px !important;
    }

    .espacado {
        margin-top: 26px;
        margin-bottom: 26px;
    }

    @media screen and (max-width: 200px) {
        .campo-pesquisa {
            display: none;
        }

        .headline-custom {
            font-size: 16px !important;
            font-weight: 400;
            letter-spacing: normal
        }
    }

    @media screen and (min-width: 768px) {
        .campo-pesquisa-xs {
            display: none;
        }
    }
</style>
