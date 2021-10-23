// Fill out your copyright notice in the Description page of Project Settings.


#include "MessageManager.h"

UMessageManager::~UMessageManager()
{
}

void UMessageManager::Initialize(FSubsystemCollectionBase& Collection)
{
	
}

void UMessageManager::RemoveListener(FName Channel, int32 ID)
{
	auto v = Listeners.Find(Channel);
	if(v){v->Remove(ID);}
}
