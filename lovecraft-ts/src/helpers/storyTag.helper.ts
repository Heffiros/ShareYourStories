import { StoryTag } from "@prisma/client"
import { IStoryTagDto } from "../schemas/StoryTag"

export const toStoryTagDto = (storyTag: StoryTag): IStoryTagDto => ({
  id: storyTag.id,
  label: storyTag.label,
  createdAt: storyTag.createdAt,
  updatedAt: storyTag.updatedAt
})