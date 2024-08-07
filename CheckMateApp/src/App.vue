<template>
  <v-app id="inspire">
    <v-navigation-drawer expand-on-hover rail v-model="drawer" app>
      <v-list>
        <v-list-item
          prepend-avatar="https://randomuser.me/api/portraits/women/85.jpg"
          subtitle="sandra_a88@gmailcom"
          title="Sandra Adams"
        ></v-list-item>
      </v-list>

      <v-divider></v-divider>

      <v-list density="compact" nav>
        <v-list-item
          prepend-icon="mdi-format-list-checks"
          title="To do"
          value="myfiles"
          to="/todo"
        ></v-list-item>
        <v-list-item
          prepend-icon="mdi-star"
          title="About"
          value="starred"
          to="/about"
        ></v-list-item>
      </v-list>
    </v-navigation-drawer>

    <v-app-bar>
      <v-app-bar-nav-icon @click="drawer = !drawer"></v-app-bar-nav-icon>

      <v-app-bar-title>CheckMate</v-app-bar-title>
    </v-app-bar>

    <v-main>
      <!--  -->
      <router-view></router-view>
    </v-main>
  </v-app>
</template>

<script setup>
import "./styles/app.css";
import "./styles/settings.scss";
import { ref } from "vue";

const drawer = ref(null);
</script>

<script>
export default {
  data: () => ({ drawer: null }),
};
</script>

<style>
.v-app-bar-title {
  font-weight: 900 !important;
}
</style>
