<template>
  <a-form layout="inline">
    <a-row :gutter="[24, 24]">
      <template v-for="slotItem in defaultShowSlots" :key="slotItem">
        <a-col :md="8" :sm="24">
          <a-form-item :label="labelMap[slotItem] || ''">
            <slot :name="slotItem" :setLabel="setLabel(slotItem)"></slot>
          </a-form-item>
        </a-col>
      </template>
      <template v-if="advanced">
        <template v-for="slotItem in advancedShowSlots" :key="slotItem">
          <a-col :md="8" :sm="24">
            <a-form-item :label="labelMap[slotItem] || ''">
              <slot :name="slotItem" :setLabel="setLabel(slotItem)"></slot>
            </a-form-item>
          </a-col>
        </template>
      </template>
      <a-col :md="{ span: 8, offset: calcSubBtnOffset(8) }" :sm="24">
        <span
          class="table-page-search-submitButtons"
          :style="(advanced && { float: 'right', overflow: 'hidden' }) || {}"
        >
          <slot name="buttons"></slot>
          <template v-if="Object.keys($slots).length * 8 > 24">
            <a style="margin-left: 8px" @click="toggleAdvanced">
              {{ advanced ? '收起' : '展开' }}
              <DownOutlined class="collapse-icon" />
            </a>
          </template>
        </span>
      </a-col>
    </a-row>
  </a-form>
</template>

<script lang="ts">
import { defineComponent, reactive, toRefs, computed } from 'vue'
import { DownOutlined } from '@ant-design/icons-vue'

export default defineComponent({
  name: 'TableQueryHeader',
  components: { DownOutlined },
  props: {
    /** 默认显示个数 */
    defaultShowItems: {
      type: Number,
      default: 2
    }
  },
  setup(props, { slots }) {
    const state = reactive({
      advanced: false,
      labelMap: {}
    })

    /**
     * @description 默认显示的插槽
     */
    const defaultShowSlots = computed(() => {
      return Object.keys(slots).slice(0, props.defaultShowItems)
    })

    /**
     * @description 显示更多的插槽
     */
    const advancedShowSlots = computed(() => {
      return Object.keys(slots)
        .slice(props.defaultShowItems)
        .filter((key) => key !== 'buttons')
    })

    /**
     * @description 展开/收起 图标旋转转数
     */
    const turn = computed(() => `${state.advanced ? 0.5 : 0}turn`)

    /**
     * @description 设置表单项label
     */
    const setLabel = (slotItem) => {
      return (label) => {
        state.labelMap[slotItem] = label
      }
    }

    /**
     * @description 切换展开/收起 状态
     */
    const toggleAdvanced = () => {
      state.advanced = !state.advanced
    }

    /**
     * @param {number} span 栅格占位格数
     */
    const calcSubBtnOffset = (span: number) => {
      const total = (state.advanced ? Object.keys(slots).length : props.defaultShowItems) * span
      const remainder = total % 24
      if (total < 24 || remainder === 0) {
        return 0
      }
      return 24 - remainder
    }

    return {
      ...toRefs(state),
      turn,
      defaultShowSlots,
      advancedShowSlots,
      calcSubBtnOffset,
      setLabel,
      toggleAdvanced
    }
  }
})
</script>

<style lang="scss" scoped>
.table-page-search-submitButtons {
  display: block;
  margin-bottom: 24px;
  white-space: nowrap;
}

::v-deep .ant-row {
  width: 100%;
}

::v-deep .ant-form-item {
  margin-right: 0;
}

.collapse-icon {
  transform: rotate(v-bind(turn));
  transition: transform 0.3s;
}
</style>
