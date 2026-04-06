<script setup lang="ts">
const router = useRouter()
</script>
<template>
  <section class="h-screen d-flex flex-wrap justify-center align-center">
    <VContainer max-width="480">
      <VRow justify="center" class="mb-6">
        <VCol cols="auto">
          <h1 class="text-h4 font-weight-bold text-center">BingoGo</h1>
          <p class="text-subtitle-1 text-center text-medium-emphasis">What would you like to do?</p>
        </VCol>
      </VRow>
      <VRow>
        <VCol cols="12">
          <VCard :elevation="4" class="pa-6 cursor-pointer" @click="router.push('/board/1')">
            <div class="d-flex align-center ga-4">
              <Icon name="ic:round-add-circle" size="40" class="text-primary" />
              <div>
                <div class="text-h6 font-weight-bold">Create New Board</div>
                <div class="text-body-2 text-medium-emphasis">Set up a new bingo board to play</div>
              </div>
            </div>
          </VCard>
        </VCol>
        <VCol cols="12">
          <VCard :elevation="4" class="pa-6" disabled>
            <div class="d-flex align-center ga-4">
              <Icon name="ic:round-view-list" size="40" class="text-secondary" />
              <div>
                <div class="text-h6 font-weight-bold">List Boards</div>
                <div class="text-body-2 text-medium-emphasis">
                  Browse and load an existing board
                </div>
                <VChip size="x-small" class="mt-1">Coming soon</VChip>
              </div>
            </div>
          </VCard>
        </VCol>
      </VRow>
    </VContainer>
  </section>
</template>
