<script setup lang="ts">
import type { DataTableOptions, Entry, EntryCategory, EntryType, PaginatedResponse } from '@/types/types';
import { ref } from 'vue';
import EntryTable from './EntryTable.vue';
import StatCard from './StatCard.vue';
import { mdiAccount } from '@mdi/js'

const emit = defineEmits(['toggleDialog', "update:options"])
const props = defineProps<{
  entries: PaginatedResponse<Entry>,
  loadingEntries: boolean,
  types: EntryType[],
  categories: EntryCategory[],
  options: DataTableOptions
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
          <entry-table :entries="entries" :loading="loadingEntries" :options="options"
            @update:options="emit('update:options', $event)" />
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>
