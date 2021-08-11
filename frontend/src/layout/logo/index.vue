<template>
  <div class="logo">
    <img src="~@/assets/images/logo.png" alt="" />
    <h2 v-show="!collapsed" class="title">One Piece</h2>
  </div>
</template>

<script>
export default {
  name: 'Logo',
  props: {
    collapsed: {
      type: Boolean
    }
  }
}
</script>

<style lang="scss" scoped>
.logo {
  display: flex;
  height: 64px;
  padding-left: 24px;
  overflow: hidden;
  line-height: 64px;
  white-space: nowrap;
  align-items: center;

  img {
    height: 32px;
    margin-right: 8px;
  }

  .title {
    margin-bottom: 0;
    color: white;
  }
}
</style>
