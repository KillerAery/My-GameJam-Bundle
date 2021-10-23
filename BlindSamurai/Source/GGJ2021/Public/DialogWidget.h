// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "CoreMinimal.h"
#include "Blueprint/UserWidget.h"
#include "Components/Border.h"
#include "Components/Image.h"
#include "Components/TextBlock.h"


#include "DialogWidget.generated.h"

/**
 * 
 */
UCLASS()
class GGJ2021_API UDialogWidget : public UUserWidget
{
	GENERATED_BODY()
	protected:
	virtual void NativeConstruct() override;


	public:
	UDialogWidget(const FObjectInitializer& ObjectInitializer);
	//FString  DialogContent;
	UPROPERTY(EditAnywhere, BlueprintReadWrite, Category = "Text")
	FString DialogContent;
	
	UPROPERTY()
	UImage* Character_1Image;
	UPROPERTY()
	UImage* Character_2Image;
	UPROPERTY()
	UTextBlock* DialogContentTextBlock;
	UPROPERTY()
	UBorder* DialogContentBorder;
	
};
