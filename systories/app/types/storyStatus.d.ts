// Enum pour les statuts des histoires
export type StoryStatus = 'Pending' | 'Online' | 'Private' | 'Deleted' | 'ModerateByAdmin' | 'ModerateAuto' | 'Winner'

// Configuration pour l'affichage des tags de statut
export interface StoryStatusConfig {
  label: string
  classes: string
}

// Type pour la configuration des statuts (optionnelle)
export type StoryStatusConfigs = Partial<Record<StoryStatus, StoryStatusConfig>>