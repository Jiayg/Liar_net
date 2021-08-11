import { PageParams } from '@/api/ResponseModel'

export interface DictItemModel {
  category: string
  createdAt: string
  description: string
  id: number
  label: string
  updatedAt: string
}

export interface DictListModel extends PageParams {
  data: DictItemModel[]
}
