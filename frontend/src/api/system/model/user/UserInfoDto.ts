import { UserProfileDto } from './UserProfileDto'

export interface UserInfoDto {
  // 用户id
  id: number
  // 基本信息
  profile: UserProfileDto
  // 角色集合
  roles: string[]
  // 权限集合
  permissions: string[]
}
