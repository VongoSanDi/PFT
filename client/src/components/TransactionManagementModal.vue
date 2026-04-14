<script setup lang="ts">
import type { Entry, EntryCategory, EntryType } from '@/types/types';

const isDialogOpen = defineModel<boolean>('dialog')
const entry = defineModel<Entry>('entry', { required: true })
const props = defineProps<{
  title: string,
  subtitle: string,
  loading: boolean,
  types: EntryType[],
  categories: EntryCategory[]
}>()
const emit = defineEmits(['saved'])

</script>
<template>
  <v-dialog v-model="isDialogOpen" height="100%" :loading="loading">
    <v-card class="v-card">
      <v-card-title>
        {{ title }}
      </v-card-title>
      <v-card-subtitle>
        {{ subtitle }}
      </v-card-subtitle>
      <v-card-text>
        <v-row class="justify-space-around">
          <v-col cols="10">
            <v-radio-group v-model="entry.type" inline color="primary">
              <v-radio v-for="t in types" :key="t.id" :value="t" :label="t.name" />
            </v-radio-group>
          </v-col>
          <v-col cols="10">
            <v-autocomplete v-model="entry.category" label="Category" return-object :items="categories"
              item-title="name" item-value="id"></v-autocomplete>
          </v-col>
          <v-col cols="10">
            <v-number-input v-model="entry.amount" label="Amount" control-variant="hidden" :min="0.00" :precision="2" />
          </v-col>
          <v-col cols="10">
            <v-date-input v-model="entry.date" :max="new Date()" label="Date input"></v-date-input>
          </v-col>
          <v-col cols="10">
            <v-text-field v-model="entry.description" label="Description (optionnal)" clearable />
          </v-col>
        </v-row>
      </v-card-text>
      <v-card-actions>
        <v-btn text="Cancel" variant="plain" @click="isDialogOpen = false"></v-btn>
        <v-btn text="Save" @click="emit('saved')"></v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>
<style scoped></style>
