<template>
  <dynamic-table
    ref="tableRef"
    :columns="columns"
    :get-list-func="getAdminDictConfig"
    rowKey="id"
    :row-selection="rowSelection"
  >
    <template #title>
      <a-button
        v-permission="{ action: 'create', effect: 'disabled' }"
        type="primary"
        @click="addItem"
      >
        新增字典
      </a-button>
      <a-button
        v-permission="{ action: 'delete', effect: 'disabled' }"
        :disabled="isDisabled"
        type="primary"
        @click="deleteItems"
      >
        删除
      </a-button>
    </template>
  </dynamic-table>
</template>
<script lang="ts">
import { defineComponent, reactive, toRefs, createVNode, computed, ref } from 'vue'
import { Modal } from 'ant-design-vue'
import { QuestionCircleOutlined } from '@ant-design/icons-vue'
import { DynamicTable } from '@/components/dynamic-table'
import {
  delAdminDictConfig,
  getAdminDictConfig,
  patchAdminDictConfig,
  postAdminDictConfig
} from '@/api/system/dict'
import { getFormSchema } from './form-schema'
import { columns } from './columns'
import { hasPermission } from '@/utils/permission/hasPermission'
import { useFormModal } from '@/hooks/useFormModal/'

export default defineComponent({
  name: 'SystemDict',
  components: {
    DynamicTable
  },
  setup() {
    const tableRef = ref<InstanceType<typeof DynamicTable>>()!

    const state = reactive({
      tableLoading: false,
      rowSelection: {
        onChange: (selectedRowKeys, selectedRows) => {
          state.rowSelection.selectedRowKeys = selectedRowKeys
        },
        selectedRowKeys: []
      }
    })

    // 删除多项
    const deleteItems = () => {
      Modal.confirm({
        title: '提示',
        icon: createVNode(QuestionCircleOutlined),
        content: '您确定要删除所有选中吗？',
        onOk: async () => {
          await delAdminDictConfig(state.rowSelection.selectedRowKeys.toString())
          tableRef.value?.refreshTableData()
          state.rowSelection.selectedRowKeys = []
        }
      })
    }
    // 添加字典
    const addItem = () => {
      useFormModal({
        title: '添加字典',
        formSchema: getFormSchema(),
        handleOk: async (modelRef, state) => {
          await postAdminDictConfig(modelRef)
          tableRef.value?.refreshTableData()
        }
      })
    }
    const isDisabled = computed(() => state.rowSelection.selectedRowKeys.length == 0)

    return {
      ...toRefs(state),
      columns,
      tableRef,
      isDisabled,
      getAdminDictConfig,
      addItem,
      deleteItems
    }
  }
})
</script>
