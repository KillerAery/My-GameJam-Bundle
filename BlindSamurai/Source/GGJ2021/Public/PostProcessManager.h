// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "CoreMinimal.h"
#include "Subsystems/GameInstanceSubsystem.h"
#include "PostProcessManager.generated.h"

/**
 * 
 */

class UMaterialParameterCollection;


UCLASS()
class GGJ2021_API UPostProcessManager : public UGameInstanceSubsystem
{
	GENERATED_BODY()
public:
	// Ĭ�ϴ���
	virtual bool ShouldCreateSubsystem(UObject* Outer) const override { return true; }
	~UPostProcessManager();
	// ��ʼ������
	virtual void Initialize(FSubsystemCollectionBase& Collection) override;


};
