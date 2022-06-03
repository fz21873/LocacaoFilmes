<template>
    <div class="largura100">
        <div v-show="larguraTela >= breakpoint">
            <slot name="desktop"></slot>
        </div>
        <div v-show="larguraTela < breakpoint">
            <v-progress-linear v-if="carregando" height="4" :indeterminate="true" style="margin: 5px 0;"></v-progress-linear>
            <v-menu v-if="ordenarPor != null" offset-y  :close-on-content-click="false">
                <template v-slot:activator="{ on }">
                    <v-btn v-on="on" text class="filtro-menu-botao">
                        <v-icon>swap_vert</v-icon> Ordenar por
                    </v-btn>
                </template>
                <v-list>
                    <div v-for="(item,key, index) in cabecalho" :key="index">
                        <v-list-item v-if="item.sortable" @click="ordenarDados(item)">
                            <v-list-item-title>
                                {{ item.text }}
                                <v-icon>{{ ordenarDadosItem(item.value) }}</v-icon>
                            </v-list-item-title>
                        </v-list-item>
                    </div>
                </v-list>
            </v-menu>
            <v-divider v-if="ordenarPor != null"></v-divider>

        </div>
    </div>
</template>

<script>
    export default {

        name: 'listagemResponsiva',
        props: {
            // Define quando fazer a troca.
            breakpoint: {
                type: Number,
                required: false,
                default: 768
            },
            cabecalho: {
                type: Array,
                required: true
            },
            carregando: {
                type: Boolean,
                required: false
            },
            ordenarPor: {
                required: false
            },
            descendente: {
                required: false
            },


        },
        data() {
            return {
                larguraTela: window.innerWidth
            };
        },
        methods: {
            // Arrumar o layout conforme a largura da tela for mudando.
            adicionarListenerLargura() {
                window.addEventListener('resize', e => {
                    this.larguraTela = window.innerWidth;
                });
            },
            // Método para ordenar os dados na listagem.
            ordenarDados(item) {
                this.$emit('update:ordenarPor', item.value);
                this.$emit('update:descendente', !this.descendente);
            },
            // O método abaixo ordena as setas que ficam no menu.
            ordenarDadosItem(valor) {
                if (this.ordenarPor == valor) {
                    if (this.descendente) {
                        return 'arrow_downward';
                    } else {
                        return 'arrow_upward';
                    }
                }
                return '';
            }
        },
        created() {
            this.adicionarListenerLargura();
        }
    };
</script>

<style>


    .filtro-menu-botao {
        height: 50px;
        margin: 0;
        width: 100%;
    }

    .filtro-menu-botao .v-btn__content {
        justify-content: left;
    }

    .caixa-espaco {
        padding: 0 12px;
    }

    .largura100 {
        width: 100%;
    }
</style>
