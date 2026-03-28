<script setup lang="ts">
import TransactionManagementModal from '@/components/TransactionManagementModal.vue'
import TransactionType from '@/components/TransactionType.vue'
import type { CreateEntry, Entry } from '@/types/types'
import { useFetch } from '@vueuse/core'
import { ref } from 'vue'

const toggleDialog = ref(false)

const entry = ref<CreateEntry>()

const handleToggleDialog = () => {
  toggleDialog.value = true
}

const handleDialogClosed = () => {
  console.log("Closed")
  toggleDialog.value = false
}

const handleDialogSaved = async (e: Entry) => {
  // TODO the save
  console.log('SAVE HOMEVIEW', e)
  entry.value = {
    amount: e.amount,
    description: e.description,
    date: e.date,
    typeId: e.type!.id,
    categoryId: e.category!.id
  };

  await execute()
  handleDialogClosed()
  console.log('DATA', data)
}

const { data, execute } = useFetch<Entry>(`${import.meta.env.VITE_SERVER_URL}api/entries`, { immediate: false }).post(entry).json()


</script>
<template>
  <v-sheet class="mx-auto">
    <transaction-type @toggleDialog="handleToggleDialog" />
  </v-sheet>
  <transaction-management-modal v-model="toggleDialog" title="Add transaction" subtitle="Record your expense or income"
    @closed="handleDialogClosed" @saved="handleDialogSaved" />
</template>
