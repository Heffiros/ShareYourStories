export interface IPostBadgeDto {
  label: string
  emptyBadgeUrl: string
  badgeUrl: string
  description: string
}

export interface IBadgeDto {
  id: number,
  label: string
  emptyBadgeUrl: string
  badgeUrl: string
  description: string,
  createdAt: Date,
  updatedAt: Date
}