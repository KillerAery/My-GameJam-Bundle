// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "CoreMinimal.h"

#include "Engine/DataTable.h"
#include "Subsystems/GameInstanceSubsystem.h"
#include "DialogManager.generated.h"

/**
 * 
 */
UENUM(BlueprintType,Blueprintable)
enum class EDialogState:uint8
{
	Init,
    BeforeDialogBegin,
    DialogBegin,
    Typing,
    Finish
};
USTRUCT(BlueprintType)
struct FDialogDataRow : public FTableRowBase
{
	GENERATED_USTRUCT_BODY()
public:
	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "DialogDataTable")
	int32 Character_1;
	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "DialogDataTable")
	int32 Character_2;
	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "DialogDataTable")
	FString Content;
	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "DialogDataTable")
	UTexture2D* Character_1Tex;
	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "DialogDataTable")
	UTexture2D* Character_2Tex;
};

// 使用方法：找一个物体tick，比如关卡蓝图去tick  TickListenStateChange
// 接受鼠标点击推进对话，调用  ReceiveClickSignal
// 尚未实现的功能，暂停场景、打字机效果、对话框背景图
UCLASS()
class GGJ2021_API UDialogManager : public UGameInstanceSubsystem
{
	GENERATED_BODY()
public:
	// 默认创建
	virtual bool ShouldCreateSubsystem(UObject* Outer) const override{ return true; }
	~UDialogManager();
	// 初始化函数
	virtual void Initialize(FSubsystemCollectionBase& Collection)override;
	
	// 创建一段对话
	UFUNCTION(BlueprintCallable)
    void CreateDialog(FString TablePath);
	
	// 对话框的运行状态
	UPROPERTY(VisibleAnywhere)
	EDialogState PanelState;



	UFUNCTION(BlueprintCallable)
    void TickListenStateChange(float DeltaSeconds);
	UFUNCTION(BlueprintCallable)
    void PrintTest();
	UFUNCTION(BlueprintCallable)
    void ChangeState(EDialogState State)
	{
		PanelState = State;
	}
	UFUNCTION(BlueprintCallable)
	void ReceiveClickSignal(bool bIsClick);
	// 打字机效果
	UFUNCTION(BlueprintCallable)
    void ChangeTypeInterval(float Interval)
	{
		TypeInterval = Interval;
	}
	
private:

	void InitPanel();
	void SetCharacter_1Visibility(ESlateVisibility Visibility);
	void SetCharacter_2Visibility(ESlateVisibility Visibility);
	void SetTextBlockVisibility(ESlateVisibility Visibility);
	void SetBorderVisibility(ESlateVisibility Visibility);
	void LoadDialogContent(FString DialogContent) const;
	void LoadCharacter_1Tex(UTexture2D* CharacterTex) const;
	void LoadCharacter_2Tex(UTexture2D* CharacterTex) const;

	/** Dialog Blueprint class*/
	UPROPERTY(EditAnywhere)
	TSubclassOf<UUserWidget> DialogWidgetClass;

	/** 用作为界面的实例。 */
	UPROPERTY(EditAnywhere)
	UUserWidget* CurrentWidget;

	int CurrentRow;
	int MaxRow;
	bool bIsClicked;
	int CurrentTypeIndex;
	int MaxTypeIndex;
	float TypeInterval;
	//TArray 
	UPROPERTY(VisibleAnywhere)
	UDataTable* CurrentDataTable;

	
	TArray<FName> RowNames;
	FName* NameData;
	FString FirstTable = "DataTable'/Game/Data/DialogBegin.DialogBegin'";
	UPROPERTY()
	UTexture2D* Character_1Tex;
	UPROPERTY()
	UTexture2D* Character_2Tex;
};
