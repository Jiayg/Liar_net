export interface ResponseModel<T = any> {
  success: boolean
  code: number
  message: string
  result: T
}

export interface PageParams {
  pageNumber: number
  pageSize: number
  total: number
}
