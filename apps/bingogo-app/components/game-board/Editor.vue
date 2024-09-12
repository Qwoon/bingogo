<script setup lang="ts">
import type { Board, BoardTile } from '~/domain'
import { BoardForm, boardValidationSchema } from '~/forms'

interface Props {
  board?: Board.Props
  resetOnSubmit: boolean
}

interface Emits {
  (e: 'onSubmit', value: BoardForm): void
}

const props = withDefaults(defineProps<Props>(), {
  resetOnSubmit: true
})
const emit = defineEmits<Emits>()

const { handleSubmit, errors, resetForm } = useForm({
  validationSchema: boardValidationSchema,
  initialValues: props.board ? props.board : null
})

const isDirty = useIsFormDirty()
let { value: title } = useField<string>('title')
let { value: allowMultiplayer } = useField<boolean>('allowMultiplayer')
let { remove, push, fields } = useFieldArray<BoardTile.Props>('tiles')

const submit = handleSubmit(async (form: BoardForm) => {
  emit('onSubmit', form)

  if (props.resetOnSubmit) {
    while (fields.value.length !== 0) remove(0)
    resetForm({})
  }

  useNotificationStore().setMessage('Board created')
})

onBeforeRouteLeave((to, from, next) => {
  if (isDirty.value) {
    const answer = window.confirm('Unsaved progress may be lost. Are you sure you want to leave?')

    if (answer) next()
    else next(false)
  } else {
    next()
  }
})

function onTileCreateClick(): void {
  push({
    title: '',
    isChecked: false,
    points: 0,
    boardId: 0
  })
}
</script>

<template>
  <VCard title="Board creator" subtitle="Create your own unique bingo board" class="bg-grey-50">
    <VCardText>
      <VRow>
        <VCol cols="6">
          <VTextField label="Title" v-model="title" :error-messages="errors.title"></VTextField>
        </VCol>
        <VCol cols="6">
          <VCheckbox label="Allow multiplayer" v-model="allowMultiplayer" value="value"></VCheckbox>
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
        <VCol cols="12">
          <VBtn
            color="secondary"
            class="border-dashed"
            variant="outlined"
            block
            @click="onTileCreateClick"
            >Add tile</VBtn
          >
        </VCol>
      </VRow>
    </VCardText>
    <VCardText>
      <VRow>
        <VCol class="text-end">
          <VBtn @click="submit">Submit</VBtn>
        </VCol>
      </VRow>
    </VCardText>
  </VCard>
</template>
