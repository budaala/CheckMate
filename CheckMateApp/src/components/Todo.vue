<template>
  <v-row id="Todo-list-item-row">
    <v-col cols="2">
      <v-checkbox-btn :model-value="completed" @update:model-value="updateCheckbox"></v-checkbox-btn>
    </v-col>
    <v-col cols="10">
      <v-list-item-title>{{ title }}</v-list-item-title>
      <v-list-item-subtitle v-if="subtitle">{{subtitle}}</v-list-item-subtitle>
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
        title() {
            return this.todo.title;
        },
        subtitle() {
            return this.todo.subtitle;
        },
        completed: {
            get() {
                return this.todo.completed;
            },
            set(value) {
                this.todo.completed = value;
            },
        },
    },
    methods: {
        updateCheckbox(value) {
            this.$emit('update:completed', this.todo.id, value);
        },
    },
};
</script>

<style scoped>

#Todo-list-item-row {
  display: flex;
  align-items: center;
}

</style>
