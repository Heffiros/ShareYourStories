export interface Event {
  id: number
  title: string
  rules: string
  coverUrl: string
  dateBegin: string // ISO string pour les dates en front
  dateEnd: string
  hasAlreadyParticipate: boolean
  createdAt: string
  updatedAt: string
}

// Type pour la création d'un event (POST)
export interface CreateEvent {
  title: string
  rules: string
  coverUrl: string
  dateBegin: string
  dateEnd: string
}