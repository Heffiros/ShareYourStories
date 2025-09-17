
export interface IStoryTagDto {
  id: number
  label: String
  createdAt: Date
  updatedAt: Date
}

export type IPostStoryTagDto = Omit<IStoryTagDto, "id" | "createdAt" | "updatedAt">