<template>
    <v-dialog v-model="carregando" :width="largura" persistent>
        <v-card>
            <v-card-text style="overflow-y: hidden; text-align: center;">
                <v-progress-circular class="carregandoGirar" indeterminate :color="corProgresso" :size="tamanho"></v-progress-circular>
                <p>{{ mensagem }}</p>
            </v-card-text>
        </v-card>
    </v-dialog>
</template>

<script>
    export default {
        props: {
            largura: {
                type: Number,
                required: false,
                default: 240
            },
            corProgresso: {
                type: String,
                required: false,
                default: 'tecmicro'
            },
            tamanho: {
                type: Number,
                required: false,
                default: 50
            },
            mensagem: {
                type: String,
                required: false,
                default: 'Aguarde...'
            },
            carregando: {
                type: Boolean,
                required: false
            }
        },
        data() {
            return {

            };
        }
    };
</script>

<style>
    .carregandoGirar {
        display: block;
        margin: 20px auto;
    }
</style>
