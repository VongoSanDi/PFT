<script setup lang="ts">
import TransactionManagementModal from '@/components/TransactionManagementModal.vue'
import TransactionType from '@/components/TransactionType.vue'
import { useCategories } from '@/composables/apis/useCategories'
import { useEntries } from '@/composables/apis/useEntries'
import { useTypes } from '@/composables/apis/useTypes'
import type { CreateEntry, Entry } from '@/types/types'
import { computed, onMounted, ref } from 'vue'

const { create, fetchAll: loadEntries, entries } = useEntries();
const { fetchAll: loadTypes, types, loading: loadingTypes } = useTypes()
const { fetchAll: loadCategories, categories, loading: loadingCategories } = useCategories()

const toggleDialog = ref(false)

const entry = ref<Entry>({
  id: 0,
  type: null,
  typeId: 0,
  category: null,
  categoryId: 0,
  amount: 0,
  date: new Date(),
  description: ""
})

const handleToggleDialog = () => {
  toggleDialog.value = true
}

/**
 *
 */
const handleDialogSaved = async () => {
  try {
    const payload: CreateEntry = {
      amount: entry.value.amount,
      description: entry.value.description,
      date: entry.value.date,
      typeId: entry.value.type!.id,
      categoryId: entry.value.category!.id
    };

    await create(payload)
  } catch (err) {
    console.error('err', err);
  } finally {
    await loadEntries()
    toggleDialog.value = false
  }
}

onMounted(async () => {
  await Promise.all([loadTypes(), loadCategories(), fetchEntries()])

  //
  // if (types.value.length > 0) {
  //   console.log('types', types.value)
  //   entry.value.type = types.value[0]!
  // }
})

const fetchEntries = async () => {
  await loadEntries()
  if (types.value.length > 0) {
    entries.value = entries.value.map((e) => ({
      ...e,
      type: types.value.find(t => t.id === e.typeId) ?? null,
      category: categories.value.find(c => c.id === e.categoryId) ?? null,
    }))
  }
}

const isLoadingModal = computed(() => {
  return loadingTypes.value || loadingCategories.value
})

</script>
<template>
  <v-sheet class="mx-auto">
    <transaction-type @toggleDialog="handleToggleDialog" :entries="entries" :types="types" :categories="categories" />
  </v-sheet>
  <transaction-management-modal v-model:dialog="toggleDialog" v-model:entry="entry" title="Add transaction"
    subtitle="Record your expense or income" @saved="handleDialogSaved" :types="types" :categories="categories"
    :loading="isLoadingModal" />
</template>
