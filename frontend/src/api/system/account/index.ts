import http from '@/utils/http/axios'
import { BasicResponseModel } from '@/api/BasicResponseModel'
import { LoginParams, UserInfoDto, UserTokenDto } from '../model/user/Index'

enum api {
  account = '/account',
  logout = '/logout'
}

/**
 * @description: 用户登录
 */
export function login(params: LoginParams) {
  return http.request<BasicResponseModel<UserTokenDto>>(
    {
      url: api.account,
      method: 'POST',
      params
    },
    {
      isTransformRequestResult: false
    }
  )
}

/**
 * @description: 获取用户信息
 */
export function getUserInfo() {
  return http.request<BasicResponseModel<UserInfoDto>>(
    {
      url: api.account,
      method: 'GET'
    },
    {
      isTransformRequestResult: false
    }
  )
}

/**
 * @description: 用户修改密码
 */
export function changePassword(params, uid) {
  return http.request(
    {
      url: `/user/u${uid}/changepw`,
      method: 'POST',
      params
    },
    {
      isTransformRequestResult: false
    }
  )
}

/**
 * @description: 用户登出
 */
export function logout(params) {
  return http.request({
    url: api.logout,
    method: 'POST',
    params
  })
}
