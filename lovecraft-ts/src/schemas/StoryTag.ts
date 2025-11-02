
export interface IStoryTagDto {
  id: number
  label: string
  createdAt: Date
  updatedAt: Date
}

export type IPostStoryTagDto = Omit<IStoryTagDto, "id" | "createdAt" | "updatedAt">