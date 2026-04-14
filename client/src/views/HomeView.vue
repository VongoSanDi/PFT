<script setup lang="ts">
import TransactionManagementModal from '@/components/TransactionManagementModal.vue'
import TransactionType from '@/components/TransactionType.vue'
import { useCategories } from '@/composables/apis/useCategories'
import { useEntries } from '@/composables/apis/useEntries'
import { useTypes } from '@/composables/apis/useTypes'
import type { CreateEntry, DataTableOptions, Entry } from '@/types/types'
import { computed, onMounted, ref, watch } from 'vue'

const { create, fetchAll: loadEntries, entries, loading: loadingEntries } = useEntries();
const { fetchAll: loadCategories, categories, loading: loadingCategories } = useCategories()
const { fetchAll: loadTypes, types: types, loading: loadingTypes } = useTypes()

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

const tableOptions = ref<DataTableOptions>({
  page: 1,
  itemsPerPage: 10,
  search: undefined,
  sortBy: []
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
    await fetchEntries()
    toggleDialog.value = false
  }
}

onMounted(async () => {
  await Promise.all([loadTypes(), loadCategories(), fetchEntries()])

  // Par defaut on met que c'est une dépense vue que dans 99% des cas c'est le cas xD
  if (types.value.length > 0) {
    entry.value.type = types.value[0]!
  }
})

const fetchEntries = async () => {
  console.log('options', tableOptions.value)
  await loadEntries(tableOptions.value)

  // On map correctement les types/categories car on recois juste les ids de la db
  if (types.value.length > 0) {
    entries.value.data = entries.value?.data.map((e) => ({
      ...e,
      type: types.value.find(t => t.id === e.typeId) ?? null,
      category: categories.value.find(c => c.id === e.categoryId) ?? null,
    }))
  }
}

const isLoadingModal = computed(() => {
  return loadingTypes.value || loadingCategories.value
})

watch(tableOptions, async () => {
  await fetchEntries()
}, { deep: true }
)

</script>
<template>
  <v-sheet class="mx-auto">
    <transaction-type v-model:options="tableOptions" @toggleDialog="handleToggleDialog" :entries="entries"
      :types="types" :categories="categories" :loading-entries="loadingEntries" />
  </v-sheet>
  <transaction-management-modal v-model:dialog="toggleDialog" v-model:entry="entry" title="Add transaction"
    subtitle="Record your expense or income" @saved="handleDialogSaved" :types="types" :categories="categories"
    :loading="isLoadingModal" />
</template>
