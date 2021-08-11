<template>
  <div class="login-box">
    <div class="login-logo">
      <!--      <svg-icon name="logo" />-->
      <img src="~@/assets/images/logo.png" alt="" />
      <h1>Antd Admin</h1>
    </div>
    <a-form layout="horizontal" :model="formInline" @submit="handleSubmit">
      <a-form-item>
        <a-input v-model:value="formInline.username" size="large" placeholder="admin">
          <template #prefix><user-outlined type="user" /></template>
        </a-input>
      </a-form-item>
      <a-form-item>
        <a-input
          v-model:value="formInline.password"
          size="large"
          type="password"
          placeholder="123456"
          autocomplete="new-password"
        >
          <template #prefix><lock-outlined type="user" /></template>
        </a-input>
      </a-form-item>
      <a-form-item>
        <a-button type="primary" html-type="submit" size="large" :loading="loading" block>
          登录
        </a-button>
      </a-form-item>
    </a-form>
  </div>
</template>

<script lang="ts">
import { defineComponent, reactive, toRefs } from 'vue'
import { message } from 'ant-design-vue'
import { UserOutlined, LockOutlined } from '@ant-design/icons-vue'
// import md5 from 'blueimp-md5'
// ~@/assets/logo.png
import { useRoute, useRouter } from 'vue-router'
import { useStore } from '@/store'

import { SvgIcon } from '@/components/svg-icon'

export default defineComponent({
  name: 'Login',
  components: { UserOutlined, LockOutlined, SvgIcon },
  setup() {
    const state = reactive({
      loading: false,
      formInline: {
        username: '',
        password: ''
      }
    })

    const store = useStore()
    const router = useRouter()
    const route = useRoute()

    const handleSubmit = async () => {
      const { username, password } = state.formInline
      if (username.trim() == '' || password.trim() == '')
        return message.warning('用户名或密码不能为空！')
      message.loading('登录中...', 0)
      state.loading = true
      console.log(state.formInline)
      const params = {
        username,
        password
      }
      // params.password = md5(password)
      const { code, message: msg } = await store.dispatch('user/login', params).finally(() => {
        state.loading = false
        message.destroy()
      })
      if (code == 0) {
        const toPath = decodeURIComponent((route.query?.redirect || '/') as string)
        message.success('登录成功！')
        router.replace(toPath).then((_) => {
          if (route.name == 'login') {
            router.replace('/')
          }
        })
      } else {
        message.info(msg || '登录失败')
      }
    }
    return {
      ...toRefs(state),
      handleSubmit
    }
  }
})
</script>

<style lang="scss" scoped>
.login-box {
  display: flex;
  width: 100vw;
  height: 100vh;
  padding-top: 240px;
  background: url('../../../assets/login.svg');
  background-size: 100%;
  flex-direction: column;
  align-items: center;

  .login-logo {
    display: flex;
    margin-bottom: 30px;
    align-items: center;

    .svg-icon {
      font-size: 48px;
    }

    img {
      height: 48px;
    }

    h1 {
      margin-bottom: 0;
      margin-left: 10px;
    }
  }

  ::v-deep(.ant-form) {
    width: 400px;

    .ant-col {
      width: 100%;
    }

    .ant-form-item-label {
      padding-right: 6px;
    }
  }
}
</style>
