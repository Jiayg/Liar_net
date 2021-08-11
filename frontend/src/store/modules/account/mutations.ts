import { UserState } from './state'

export const mutations = {
  setToken: (state: UserState, token: string) => {
    state.token = token
  },
  setAvatar: (state: UserState, avatar: string) => {
    state.avatar = avatar
  },
  setRoles: (state: UserState, roles) => {
    state.roles = roles
  },
  setPermissions: (state: UserState, permissions) => {
    state.permissions = permissions
  },
  setUserInfo: (state: UserState, info) => {
    state.info = info
  }
}
