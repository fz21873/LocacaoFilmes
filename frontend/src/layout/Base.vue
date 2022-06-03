<template>
  <v-app id="inspire">

    <v-navigation-drawer v-model="drawer" app>
      <!--  -->
      <v-list>
        <div v-for="item in itensMenu" :key="item.titulo">
          <v-list-item v-on="on" :to="item.rota" active-class="item-ativo">
            <v-list-item-icon>
              <v-icon v-text="item.icone"></v-icon>
            </v-list-item-icon>
            <!--<v-list-item-title>Home</v-list-item-title>-->

            <v-list-item-content>
              <v-list-item-title v-text="item.titulo"></v-list-item-title>
            </v-list-item-content>
          </v-list-item>

        </div>

      </v-list>

    </v-navigation-drawer>

    <v-app-bar app color="deep-purple accent-4" dense dark>
      <v-app-bar-nav-icon @click="drawer = !drawer"> </v-app-bar-nav-icon>

      <v-toolbar-title>Application</v-toolbar-title>
    </v-app-bar>

    <v-main>
      <router-view app></router-view>
    </v-main>
  </v-app>
</template>

<script>
export default {
  data: () => (
    {
      drawer: null,
      group: null,
      itensMenu: [{
        icone: '',
        titulo: 'Home',
        rota: '/',

      },

      {
        icone: '',
        titulo: 'Diretor',
        rota: '/diretor',
      },

      {
        icone: '',
        titulo: 'Filme',
        rota: '/filme',

      },


      ],
    }),

};
</script>

<style>
.item-ativo {
        background-color: #f5f5f5;
        border-left: 5px solid #10284c;
        font-weight: 700 !important;
    }
.color-interno {

  background: #FFF;
}
</style>-->
