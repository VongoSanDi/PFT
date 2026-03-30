import { useFetch } from "@vueuse/core";
import type { Ref } from "vue";

export function useApi<T>(endpoint: string,) {
  const url = import.meta.env.VITE_SERVER_URL + endpoint;

  const create = <P>(payload: Ref<P> | P) => useFetch<T>(url, { immediate: false }).post(payload).json()

  const retrieve = () => useFetch<T>(url, { immediate: false }).get().json()

  return { create, retrieve }
}
