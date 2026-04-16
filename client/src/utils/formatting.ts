/**
 *  @param {string} value date
 *  L'API nous renvoie une date au format 2026-04-16T07:39:42.07+00:00
 *  Mais nous voulons juste afficher la date, mais je prefere garder les heures dans l'objet de base
 *  Renvoie la date au bon format pour l'affichage à l'utilisateur
 */
export const formatEntryDate = (value: string | Date) => {
  return new Intl.DateTimeFormat().format(new Date(value))
}
