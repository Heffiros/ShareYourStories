import { IStoryCommentDto, StoryCommentWithRelations } from "src/schemas/StoryComment"
import { toUserDto } from '../helpers/user.helper'

export const toStoryCommentDto = (storyComment: StoryCommentWithRelations): IStoryCommentDto => ({
  id: storyComment.id,
  text: storyComment.text,
  userId: storyComment.userId,
  user: toUserDto(storyComment.user),
  status: storyComment.status,
  storyId: storyComment.storyId,
  createdAt: storyComment.createdAt,
  updatedAt: storyComment.updatedAt
})