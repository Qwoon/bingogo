<script setup lang="ts">
import type { BoardTile } from '~/api';
import { boardValidationSchema } from '~/forms';

const { handleSubmit, errors, meta } = useForm({
  validationSchema: boardValidationSchema,
});

let { value: name } = useField<string>('name');
let { value: allowMultiplayer } = useField<boolean>('allowMultiplayer');
let { remove, push, fields } = useFieldArray<BoardTile.Props>('tiles');

onBeforeRouteLeave((to, from, next) => {
  if (meta.value.dirty) {
    const answer = window.confirm(
      'Unsaved progress may be lost. Are you sure you want to leave?'
    );

    if (answer) next();
    else next(false);
  } else {
    next();
  }
});

function onTileCreateClick(): void {
  push({
    title: '',
    isChecked: false,
    points: 0,
    boardId: 0,
  });
}
</script>

<template>
  <VContainer>
    <VCard
      title="Board creator"
      subtitle="Create your own unique bingo board"
      class="bg-grey-50"
    >
      <VCardText>
        <VRow>
          <VCol cols="6">
            <VTextField
              label="Name"
              v-model="name"
              :error-messages="errors.name"
            ></VTextField>
          </VCol>
          <VCol cols="6">
            <VCheckbox
              label="Allow multiplayer"
              v-model="allowMultiplayer"
              value="value"
            ></VCheckbox>
          </VCol>
          <VCol cols="12" v-for="(field, index) in fields" :key="index">
            <VTextField
              append-icon="mdi-delete-outline"
              @click:append="remove(index)"
              label="Tile name"
              v-model="field.value.title"
              :error-messages="errors[`tiles[${index}].title`]"
            >
            </VTextField>
          </VCol>
          <VCol>
            <VBtn color="secondary" block @click="onTileCreateClick"
              >Add tile</VBtn
            >
          </VCol>
        </VRow>
      </VCardText>
    </VCard>
  </VContainer>
</template>
