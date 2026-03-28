<script setup lang="ts">
import { computed } from 'vue';
import EntryTable from './EntryTable.vue';
import { mdiAccount } from '@mdi/js'

const props = defineProps<{
  title: string,
  loading: boolean,
  color?: string,
  isLastEntryCard?: boolean
}>()

const emit = defineEmits(["openEntryManagement"])

const titleTransformed = computed(() => {
  return props.title.toUpperCase()
})

</script>
<template>
  <v-card hover :color="color" :title="titleTransformed">
    <template #append v-if="isLastEntryCard">
      <v-btn @click="emit('openEntryManagement')">
        <v-icon :icon="mdiAccount"></v-icon>
      </v-btn>
    </template>
    <v-card-text v-if="isLastEntryCard">
      <entry-table />
    </v-card-text>
    <v-card-text v-else>
      € 1000.00
    </v-card-text>
  </v-card>
</template>
