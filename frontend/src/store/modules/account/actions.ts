import { ActionContext } from 'vuex'
import { UserState } from './state'
import { getUserInfo, login } from '@/api/sys/account'
import { ACCESS_TOKEN, CURRENT_USER, IS_LOCKSCREEN } from '@/store/mutation-types'
import { Storage } from '@/utils/Storage'
import store from '@/store'
import { IStore } from '@/store/types'

export const actions = {
  // 登录
  async login({ commit }: ActionContext<UserState, IStore>, userInfo) {
    try {
      const params = {
        account: userInfo.username,
        password: userInfo.password
      }
      const tokenRes = await login(params)
      const { result, success } = tokenRes

      if (!success) return Promise.resolve(tokenRes)

      console.log(result) //打印token

      const ex = 7 * 24 * 60 * 60 * 1000
      Storage.set(ACCESS_TOKEN, result.token, ex)
      Storage.set(CURRENT_USER, result, ex)
      Storage.set(IS_LOCKSCREEN, false)
      commit('setToken', result.token)

      store.commit('lockscreen/setLock', false)

      getUserInfo()
        .then((response) => {
          const { success, result } = response

          if (!success) return Promise.resolve(response)

          if (result.roles && result.permissions.length > 0) {
            commit('setRoles', result.roles)
            commit('setPermissions', result.permissions)
            commit('setUserInfo', result.profile)
          } else {
            Promise.reject(new Error('getInfo: roles must be a non-null array !'))
          }

          commit('setAvatar', result.profile.avatar)

          Promise.resolve(response)
        })
        .catch((error) => {
          Promise.reject(error)
        })

      return Promise.resolve(tokenRes)
    } catch (e) {
      return Promise.reject(e)
    }
  },

  // 获取用户信息
  getUserInfo({ commit }: ActionContext<UserState, IStore>) {
    return new Promise((resolve, reject) => {
      getUserInfo()
        .then((response) => {
          const { success, result } = response

          if (!success) return Promise.resolve(response)

          if (result.roles && result.permissions.length > 0) {
            commit('setRoles', result.roles)
            commit('setPermissions', result.permissions)
            commit('setUserInfo', result.profile)
          } else {
            Promise.reject(new Error('getInfo: roles must be a non-null array !'))
          }

          commit('setAvatar', result.profile.avatar)

          Promise.resolve(response)
        })
        .catch((error) => {
          reject(error)
        })
    })
  },

  // 登出
  async logout({ commit }: ActionContext<UserState, IStore>) {
    commit('setRoles', [])
    commit('setPermissions', [])
    commit('setUserInfo', '')
    Storage.remove(ACCESS_TOKEN)
    Storage.remove(CURRENT_USER)
    return Promise.resolve('')
  }
}
