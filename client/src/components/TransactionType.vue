<script setup lang="ts">
import type { Entry, EntryCategory, EntryType } from '@/types/types';
import { ref } from 'vue';
import EntryTable from './EntryTable.vue';
import StatCard from './StatCard.vue';
import { mdiAccount } from '@mdi/js'

const emit = defineEmits(['toggleDialog'])
const props = defineProps<{
  entries: Entry[],
  types: EntryType[],
  categories: EntryCategory[]
}>()

const transactionType = ref("week")
</script>
<template>
  <v-container>
    <v-row class="align-center">
      <v-col>
        <div>Overview period</div>
      </v-col>
      <v-col>
        <v-btn-toggle v-model="transactionType" color="primary" mandatory>
          <v-btn value="week">Week</v-btn>
          <v-btn value="month">Month</v-btn>
          <v-btn value="year">Year</v-btn>
        </v-btn-toggle>
      </v-col>
    </v-row>
    <v-row class="justify-center">
      <v-col cols="4" md="3">
        <StatCard title="Balance" :loading="false" color="green-lighten-1" />
      </v-col>
      <v-col cols="4" md="3">
        <StatCard title="Income" :loading="false" color="green-lighten-1" />
      </v-col>
      <v-col cols="4" md="3">
        <StatCard title="Expense" :loading="false" color="red-lighten-1" />
      </v-col>
      <v-col cols="12">
        <v-card>
          <template #append>
            <v-btn @click="emit('toggleDialog')">
              <v-icon :icon="mdiAccount"></v-icon>
            </v-btn>
          </template>
          <entry-table :entries="entries" />
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>
