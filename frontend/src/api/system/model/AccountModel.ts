import { RoleEnum } from '@/enums/roleEnum'


// 获取用户信息返回值
export interface GetUserInfoByUserIdModel {
  roles: { roleName: string; value: RoleEnum }[]
  // 用户id
  userId: string | number
  // 用户名
  username: string
  // 真实名字
  realName: string
  // 介绍
  desc?: string
}
