import { BasicPageParams } from '@/api/BasicResponseModel'

export interface AccessItem {
  id: number
  isDel: number
  createdAt: string
  updatedAt: string
  moduleName: string
  actionName: string
  icon: string
  url: string
  moduleId: number
  sort: number
  keepAlive: number
  description: string | null
  children?: boolean
}

export interface AccessResultModel extends BasicPageParams {
  data: AccessItem[]
}

export interface ModuleItem {
  id: number
  moduleName: string
}
