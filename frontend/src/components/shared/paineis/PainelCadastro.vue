<template>
    <div>
        <v-container>
            <v-card>
                <v-card-title :class="classesCabecalho">
                    <v-tooltip>
                        <template v-slot:activator="{ on }">
                            <v-btn v-on="on" :class="classesIconeVoltar" icon active-class="" @click="voltar()">
                                <v-icon>arrow_back</v-icon>
                            </v-btn>
                        </template>
                        <span>Voltar</span>
                    </v-tooltip>
                    <span class="headline-custom">{{ titulo }}</span>
                    <v-spacer></v-spacer>

                </v-card-title>

                <transition @after-enter="painelCarregou">
                    <v-progress-linear :indeterminate="true" v-if="carregandoValores" color="primary"></v-progress-linear>
                    <div v-else>

                        <v-row no-gutters fill-height>
                            <v-col cols="12">
                                <listaErros :visivel="!!errosServidor" :erros="errosServidor"></listaErros>
                            </v-col>
                        </v-row>

                        <!-- slot para ser inserido o conteudo da tela de cadastro (campos) -->
                        <slot></slot>

                         <!-- Botão de salvar caso desktop -->
                    <v-container>
                      <v-layout wrap row v-if="mostrarBotaoSalvar">
                          <v-container>
                             <v-flex xs12 mb-1>
                                <v-btn :loading="enviandoDados" :disabled="enviandoDados" id="botaoSalvar" class="success painel-cadastro-botao-salvar" @click="salvar();">
                                    <v-icon style="margin-right: 8px">
                                        done
                                    </v-icon>
                                    {{tituloBotaoSalvar}}
                                </v-btn>
                             <slot name="botoesAdicionais"></slot>
                            </v-flex>
                          </v-container>
                      </v-layout>
                    </v-container>
                    </div>
                </transition>
                <v-btn v-if="!botaoFlutuante" absolute  fab bottom right color="success">
                    <v-icon>done</v-icon>
                </v-btn>
            </v-card>
        </v-container>
        <!-- Snackbar -->
        <v-snackbar v-model="mostrarSnackBar" :top="true" :right="true" :timeout="5000"  :color="alertaCor">
            <v-icon :color="alertaCorTexto" style="margin-bottom: 1px; margin-right: 1px;">{{ alertaIcone }}</v-icon> {{ alertaTexto }}
            <v-btn :color="alertaCorTexto" text @click="mostrarSnackBar = false">FECHAR</v-btn>
        </v-snackbar>
        <v-tooltip v-if="botaoFlutuante" :right="direcaoAtiva('right')" :left="direcaoAtiva('left')" :top="direcaoAtiva('top')" :bottom="direcaoAtiva('bottom')">
            <template v-slot:activator="{ on }">
                <v-btn id="botaoSalvar" class="botao-salvar-dados" v-on="on" color="success" :loading="enviandoDados"  fixed bottom right fab @click="salvar();">
                    <v-icon>done</v-icon>
                </v-btn>
            </template>
            <span>{{ textoTooltip }}</span>
        </v-tooltip>
        <!-- Últimas alterações -->
        <v-dialog v-model="mostrarModalAlteracoes" width="320">
            <v-card>
                <v-card-title class="tecmicro white--text infoTitulo">
                    <h3>Informações do registro</h3>
                    <v-spacer></v-spacer>
                    <v-btn icon  @click="mostrarModalAlteracoes = false;">
                        <v-icon>close</v-icon>
                    </v-btn>
                </v-card-title>
                <v-card-text style="padding-top: 24px">
                    <p>
                        Criado por: {{ criadoPor }}
                        <br>
                        Criado em: {{ criadoEm }}
                    </p>
                    <p>
                        Alterado por: {{ alteradoPor }}
                        <br>
                        Alterado em: {{ alteradoEm }}
                    </p>
                </v-card-text>
            </v-card>
        </v-dialog>
    </div>
</template>

<script>

   import ListarErros from '../validar/ListarErros.vue';
    export default {
        components: {
            'listaErros': ListarErros
        },

        name: 'painelCadastro',
        props: {
            //titulo do cartao
            titulo: {
                type: String,
                required: true
            },

            errosServidor: {
                required: true
            },
            //mostra abarra de progresso quando verdadeiro
            carregandoValores: {
                type: Boolean,
                required: true
            },
            //utilizado para mostrar/esconder o toast
            alerta: {
                type: Boolean,
                required: true
            },
            //cor do toast
            alertaCor: {
                type: String,
                required: false,
                default: 'black'
            },
            //cor do texto do toast
            alertaCorTexto: {
                type: String,
                required: false,
                default: 'white'
            },
            //texto do toast
            alertaTexto: {
                type: String,
                required: true
            },
            //icone do toast
            alertaIcone: {
                type: String,
                required: false,
                default: 'white'
            },
            //controla o loading do botao salvar
            enviandoDados: {
                type: Boolean,
                required: true
            },
            //texto que aparece no tooltip do botao salvar
            textoTooltip: {
                type: String,
                required: false,
                default: 'Salvar'
            },

             //texto que aparece no tooltip do botao salvar ou no próprio botão caso flutuante = false
            tituloBotaoSalvar: {
                type: String,
                required: false,
                default: 'Salvar'
            },
            // decide se irá mostrar o botão de salvar
            mostrarBotaoSalvar: {
                type: Boolean,
                required: false,
                default: true
            },
            //classes que serao aplicadas no card-title
            classesCabecalho: {
                type: String,
                required: false,
                default: 'primary white--text'
            },

             classesIconeVoltar: {
                type: String,
                required: false,
                default: ' white--text'
            },
            mostrarBotaoDetalhes: {
                type: Boolean,
                required: false,
                default: false
            },
            criadoPor: {
                type: String,
                required: false
            },
            alteradoPor: {
                type: String,
                required: false
            },
            criadoEm: {
                required: false
            },
            alteradoEm: {
                required: false
            },
            //left,right,top,bottom
            direcaoTooltipBotaoSalvar: {
                type: String,
                required: false,
                default: 'left'
            },
            botaoFlutuante: {
                type: Boolean,
                required: false,
                default: true
            }
        },
        data() {
            return {
                mostrarSnackBar: false,
                mostrarInfo: 'esconder',
                mostrarModalAlteracoes: false,
            };
        },
        methods: {
            voltar() {
                this.$emit('botaoVoltarClicado');
            },
            salvar() {
                this.$emit('botaoSalvarClicado');
            },
            tratarTeclado(event) {
                if (!this.mostrarModalAlteracoes) {
                    this.$emit('teclaPressionada', event);
                } else if (event.keyCode == '27' || event.which == '27') {
                    this.mostrarModalAlteracoes = false;
                }

            },
            painelCarregou(event) {
                this.$emit('painelCadastroCarregou', event);
            },
            direcaoAtiva(direcao) {
                if (this.direcaoTooltipBotaoSalvar == direcao) {
                    return true;
                }
                return false;
            }
        },
        watch: {
            mostrarBotaoDetalhes() {
                if (this.mostrarBotaoDetalhes == true) {
                    this.mostrarInfo = 'mostrar';
                } else {
                    this.mostrarInfo = 'esconder';
                }
            },
            alerta() {
                this.mostrarSnackBar = this.alerta;
            },
            mostrarSnackBar() {
                this.$emit('update:alerta', this.mostrarSnackBar);
            }
        },
        beforeMount() {
            window.addEventListener('keyup', this.tratarTeclado);
        },
        beforeDestroy() {
            window.removeEventListener('keyup', this.tratarTeclado);
        }
    };
</script>

<style>
    .mostrar {
        display: block;
    }

    .esconder {
        display: none;
    }

    .infoTitulo {
        padding-bottom: 5px;
        padding-top: 5px;
        font-size: 14px !important;
    }

    .botao-salvar-dados:focus {
        border: 2px solid #2E7D32 !important;
    }

    @media screen and (max-width: 599px) {
        .campo-pesquisa {
            display: none;
        }

        .headline-custom {
            font-size: 16px !important;
            font-weight: 400;
            letter-spacing: normal
        }
    }
</style>
