<template>
  <div>
    <v-dialog class="conf" v-model="visivel" persistent max-width="400">
      <v-card tile>
        <v-toolbar v-if="Boolean(options.title)"  :color="options.color" dense text>
          <v-icon v-if="Boolean(options.icon)" left>{{ options.icon }}</v-icon>
          <v-toolbar-title class="white--text" v-text="options.title"/>
        </v-toolbar>
      <v-card-text class="body-1 text-body-1 py-3" v-html="options.mensagem"/>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn
          v-if="Boolean(options.buttonFalseText)"
          :color="buttonFalseColor"
          :text="buttonFalseFlat"
          @click="choose(false)"
        >
          {{ options.buttonFalseText }}
        </v-btn>
        <v-btn
          v-if="Boolean(options.buttonTrueText)"
          :color="buttonTrueColor"
          :text="buttonTrueFlat"
          @click="choose(true)"
        >
          {{ options.buttonTrueText }}
        </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
    <modalCarregando :carregando="excluindoDados" mensagem="Excluindo ..." />
  </div>
</template>

<script>
  import Carregando from '../../shared/paineis/Carregando.vue';
  import Confirmacao from '@/components/shared/validar/Confirmacao';;
  export default {
    components: {
      'modalCarregando': Carregando,
      'confirmacao': Confirmacao
    },
    props: {
    options: {
      buttonTrueText: '',
      buttonFalseText: '',
      mensagem: '',
      color: '',
      icon: '',
      title: ''
    },

    buttonFalseFlat: {
      type: Boolean,
      default: true
    },

    buttonTrueFlat: {
      type: Boolean,
      default: true
    },

    buttonTrueColor: {
      type: String,
      default: 'primary'
    },

    buttonFalseColor: {
      type: String,
      default: 'grey'
    },
    visivel: {
      required: false,
      type: Boolean,
      default:false
    }
  },
  data () {
    return {
      value: false,
      excluindoDados: false,
    };
  },

    mounted () {
    document.addEventListener('keyup', this.onEnterPressed);
  },

    destroyed () {
      document.removeEventListener('keyup', this.onEnterPressed);
    },
  methods: {
    onEnterPressed (e) {

      if (e.keyCode === 13) {
        e.stopPropagation();
        this.choose(true);
      }
    },

    choose (value) {
      if (value == true){
        this.excluindoDados = true;
      }
      this.$emit('result', value);
      this.value = value;
      this.$destroy();
      return this.value;
    },

    change (res) {
      this.$destroy();
    }
  }
};

</script>

<style>

</style>
