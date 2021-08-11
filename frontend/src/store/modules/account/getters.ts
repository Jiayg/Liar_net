import { UserState } from './state'

export const getters = {
  token: (state: UserState) => state.token,
  avatar: (state: UserState) => state.avatar,
  nickname: (state: UserState) => state.name,
  roles: (state: UserState) => state.roles,
  userInfo: (state: UserState) => state.info
}
