import { IStoryDto, StoryWithRelations } from '../schemas/Story'
import { toUserDto } from '../helpers/user.helper'
import mammoth from "mammoth"

export const toStoryDto = (story: StoryWithRelations): IStoryDto => ({
  id: story.id,
  title: story.title,
  coverUrl: story.coverUrl,
  summary: story.summary,
  status: story.status,
  pages: story.pages,
  userId: story.userId,
  user: toUserDto(story.user),
  eventId: story.eventId,
  event: story.event,
  teamId: story.teamId,
  team: story.team,
  storyHistory: story.storyHistories ? story.storyHistories[0] : null,
  storyTags: null,
  storyVotes: story.storyVotes,
  createdAt: story.createdAt,
  updatedAt: story.updatedAt
})

export async function extractRawTextFromDocx(fileBuffer: Buffer): Promise<string> {
  const result = await mammoth.extractRawText({ buffer: fileBuffer })
  return result.value
}
export async function splitIntoPages(text: string, maxWordsPerPage: number): Promise<string[]> {
  const lines = text.split(/\r?\n/)

  const pages: string[] = []
  let currentPage = ""
  let wordCount = 0

  for (const line of lines) {
    const words = line.split(/\s+/).filter(w => w.length > 0)

    for (const word of words) {
      currentPage += word + " "
      wordCount++

      if (wordCount >= maxWordsPerPage) {
        pages.push(currentPage.trim())
        currentPage = ""
        wordCount = 0
      }
    }
    currentPage += "</br>"
  }

  if (currentPage.trim().length > 0) {
    pages.push(currentPage.trim())
  }

  return pages
}