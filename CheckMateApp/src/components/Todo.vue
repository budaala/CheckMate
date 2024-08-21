<template>
  <v-row id="Todo-list-item-row">
    <v-col cols="2">
      <v-checkbox-btn
        :model-value="isCompleted"
        @update:model-value="$emit('update:is-completed', $event)"
        color="primary"
      ></v-checkbox-btn>
    </v-col>
    <v-col cols="10">
      <v-list-item-title>{{ todo.title }}</v-list-item-title>
      <v-list-item-subtitle v-if="todo.subtitle">{{
        todo.subtitle
      }}</v-list-item-subtitle>
    </v-col>
  </v-row>
</template>

<script>
export default {
  props: {
    todo: {
      type: Object,
      required: true,
    },
  },
  computed: {
    isCompleted: {
      get() {
        return this.todo.isCompleted;
      },
      set(value) {
        this.todo.isCompleted = value;
      },
    },
  }
};
</script>

<style scoped>
#Todo-list-item-row {
  display: flex;
  align-items: center;
}
</style>
