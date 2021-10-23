// Fill out your copyright notice in the Description page of Project Settings.


#include "PostProcessManager.h"
#include "TimerManager.h"
#include "Kismet/GameplayStatics.h"
#include "Engine/PostProcessVolume.h"
#include "Kismet/KismetMaterialLibrary.h"
#include "Materials/MaterialParameterCollection.h"

GGJ2021_API UPostProcessManager::~UPostProcessManager()
{

}

void UPostProcessManager::Initialize(FSubsystemCollectionBase& Collection)
{
	Super::Initialize(Collection);
}


