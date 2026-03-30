<script setup lang="ts">
import type { Entry } from '@/types/types';


const props = defineProps<{
  entries: Entry[]
}>()

</script>
<template>
  <v-table>
    <thead>
      <tr>
        <th class="text-left">
          Amount
        </th>
        <th class="text-left">
          Date
        </th>
        <th class="text-left">
          Description
        </th>
        <th class="text-left">
          Category
        </th>
        <th class="text-left">
          Type
        </th>
      </tr>
    </thead>
    <tbody>
      <tr v-for="item in entries" :key="item.id">
        <td>{{ item.amount }}</td>
        <td>{{ item.date }}</td>
        <td>{{ item.description }}</td>
        <td>{{ item.category?.name }}</td>
        <td>{{ item.type?.name }}</td>
      </tr>
    </tbody>
  </v-table>

</template>
