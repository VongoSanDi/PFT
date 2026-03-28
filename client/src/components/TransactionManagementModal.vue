<script setup lang="ts">
import type { CreateEntry, Entry, EntryType } from '@/types/types';
import { computed, onMounted, ref, shallowRef, toRaw } from 'vue';
import { useFetch } from '@vueuse/core'

const types = ref<EntryType[]>([])
const categories = ref<EntryType[]>([])
const isDialogOpen = defineModel<boolean>()
const props = defineProps<{
  title: string,
  subtitle: string,
}>()
const emit = defineEmits(['closed', 'saved'])

const save = () => {
  // const payload: CreateEntry = {
  //   amount: transaction.value.amount,
  //   description: transaction.value.description,
  //   date: transaction.value.date,
  //   typeId: transaction.value.type!.id,
  //   categoryId: transaction.value.category!.id
  // }
  // emit('saved', payload)
  emit('saved', transaction.value)
}

const cancel = () => {
  emit('closed')
}

const transaction = shallowRef<Entry>({
  id: 0,
  type: null,
  category: null,
  amount: 0,
  date: new Date(),
  description: ""
})

const baseUrl = import.meta.env.VITE_SERVER_URL

const { data: typesData, execute: loadTypes, isFetching: isFetchingType } = useFetch<EntryType[]>(
  `${baseUrl}api/types`, { immediate: false }
).get().json()

// Categories
const { data: categoriesData, execute: loadCategories, isFetching: isFetchingCategory } = useFetch<string[]>(
  `${baseUrl}api/categories`, { immediate: false }
).get().json()

onMounted(async () => {
  await loadTypes()
  types.value = typesData.value ?? []
  transaction.value.type = types.value[0]!

  await loadCategories()
  categories.value = categoriesData.value ?? []
})

const isLoading = computed(() => {
  return isFetchingType || isFetchingCategory
})

</script>
<template>
  <v-dialog v-model="isDialogOpen" height="100%" :loading="isLoading">
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
            <v-radio-group v-model="transaction.type" inline color="primary">
              <v-radio v-for="t in types" :key="t.id" :value="t" :label="t.name" />
            </v-radio-group>
          </v-col>
          <v-col cols="10">
            <v-autocomplete v-model="transaction.category" label="Category" return-object :items="categories"
              item-title="name" item-value="id"></v-autocomplete>
          </v-col>
          <v-col cols="10">
            <v-number-input v-model="transaction.amount" label="Amount" control-variant="hidden" :min="0.00"
              :precision="2" />
          </v-col>
          <v-col cols="10">
            <v-date-input v-model="transaction.date" :max="new Date()" label="Date input"></v-date-input>
          </v-col>
          <v-col cols="10">
            <v-text-field v-model="transaction.description" label="Description (optionnal)" clearable />
          </v-col>
        </v-row>
      </v-card-text>
      <v-card-actions>
        <v-btn text="Cancel" variant="plain" @click="cancel"></v-btn>
        <v-btn text="Save" @click="save"></v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>
<style scoped></style>
