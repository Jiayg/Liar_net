import { Storage } from '@/utils/Storage'
import { ACCESS_TOKEN, CURRENT_USER } from '@/store/mutation-types'

export type UserState = {
  token: string
  name: string
  avatar: string
  roles: string[]
  permissions: string[]
  info: any
}

export const state: UserState = {
  token: Storage.get(ACCESS_TOKEN, ''),
  name: '',
  avatar: '',
  roles: [],
  permissions: [],
  info: Storage.get(CURRENT_USER, {})
}
