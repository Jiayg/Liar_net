export interface BasicResponseModel<T = any> {
  success: boolean
  code: number
  message: string
  result: T
}
export interface BasicPageParams {
  pageNumber: number
  pageSize: number
  total: number
}
