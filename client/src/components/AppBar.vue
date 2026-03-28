<script setup lang="ts">
import { mdiAccount } from '@mdi/js'

const profil = () => {
  console.log("Profil")
}

</script>
<template>
  <v-app-bar>
    <template v-slot:prepend>
      <v-app-bar-nav-icon></v-app-bar-nav-icon>
    </template>
    <v-app-bar-title>Finance Tracker</v-app-bar-title>
    <template v-slot:append>
      <v-btn :icon="mdiAccount" @click="profil" />
    </template>
  </v-app-bar>

</template>
