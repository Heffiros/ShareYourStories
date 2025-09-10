import { Badge } from "@prisma/client"
import { IBadgeDto } from "../schemas/Badge"

export const toBadgeDto = (badge: Badge): IBadgeDto => ({
  id: badge.id,
  label: badge.label,
  emptyBadgeUrl: badge.emptyBadgeUrl,
  badgeUrl: badge.badgeUrl,
  description: badge.description,
  createdAt: badge.createdAt,
  updatedAt: badge.updatedAt
})