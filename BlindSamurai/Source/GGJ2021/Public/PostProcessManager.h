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
	// 默认创建
	virtual bool ShouldCreateSubsystem(UObject* Outer) const override { return true; }
	~UPostProcessManager();
	// 初始化函数
	virtual void Initialize(FSubsystemCollectionBase& Collection) override;


};
