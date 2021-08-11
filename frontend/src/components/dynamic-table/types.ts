import type { ColumnProps, TableProps } from 'ant-design-vue/lib/table/interface'
import type { PaginationProps } from 'ant-design-vue/lib/pagination/Pagination'
import type { ButtonProps } from 'ant-design-vue/lib/button/buttonTypes'

export interface ActionItem {
  type: 'popconfirm' | 'select' | 'button' | 'text' // 控制类型，默认为a,可选： select | button | text
  text: string
  permission: {
    // 权限
    action: 'delete'
    effect: 'disabled'
  }
  props: ButtonProps | any
  func: ({ record }, refreshTableData) => any
}

export interface Columns extends ColumnProps {
  actions?: ActionItem[]
  dataIndex: string
}

export type pageOption = Partial<PaginationProps>

export interface Props extends Omit<TableProps, 'columns'> {
  columns: Columns[]
  rowKey: string | ((record: any) => string)
  pageOption: pageOption
  getListFunc: (prams) => any
  dragColEnable: boolean // 是否可以拖拽列
  dragRowEnable: boolean // 是否可以拖拽行
}
