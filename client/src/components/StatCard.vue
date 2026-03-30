<script setup lang="ts">
import { computed } from 'vue';

const props = defineProps<{
  title: string,
  loading: boolean,
  color?: string,
  isLastEntryCard?: boolean
}>()

const titleTransformed = computed(() => {
  return props.title.toUpperCase()
})

</script>
<template>
  <v-card hover :color="color" :title="titleTransformed">
    <v-card-text>
      € 1000.00
    </v-card-text>
  </v-card>
</template>
